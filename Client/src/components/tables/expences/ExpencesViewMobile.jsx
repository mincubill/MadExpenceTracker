/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Card, ListGroup } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useNavigate } from "react-router-dom";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import 'bootstrap/dist/css/bootstrap.min.css'
import { deleteExpence, getCurrentExpences, getExpenceById } from "../../../gateway/expenceGateway";
import { Fragment, useEffect, useState } from "react";
import ReactPaginate from 'react-paginate';
import { formatAmount } from "../../../utils/numberFormatter";

export const ExpencesViewMobile = ({setExpencesId, saveOperationResult, setExpencesMonth, isMonthClosedState}) => {
    const itemsPerPage = 10
    const [expenceData, setExpenceData] = useState([])
    const [needRefresh, setNeedRefresh] = useState(false)
    const [currentItems, setCurrentItems] = useState(undefined);
    const [pageCount, setPageCount] = useState(0);
    const [itemOffset, setItemOffset] = useState(0);

    useEffect(() => {
        getCurrentExpences().then(d => {
            if(d.expence === undefined || d.expence.length === 0) {
                setExpenceData(undefined)
                return
            }
            setExpenceData(d.expence.reverse())
            setExpencesId(d.id)
            setExpencesMonth(d.runningMonth)
            setPageCount(Math.ceil(d.expence.length / itemsPerPage));
            const endOffset = itemOffset + itemsPerPage;
            setCurrentItems(d.expence.slice(itemOffset, endOffset));
        })
    }, [isMonthClosedState, needRefresh, itemOffset, itemsPerPage])

    const navigate = useNavigate()

    const viewExpence = (e) => {     
        getExpenceById(e.currentTarget.id).then(d => {
            const data = {
                id: d.id,
                name: d.name,
                date: d.date,
                expenceType: d.expenceType,
                amount: d.amount
            }
            navigate("/expence", {state: { data, isReadOnly: true, isUpdate: false } })
        })
    }

    const updateExpence = (e) => {
        getExpenceById(e.currentTarget.id).then(d => {
            const data = {
                id: d.id,
                name: d.name,
                date: d.date,
                expenceType: d.expenceType,
                amount: d.amount
            }
            navigate("/expence", {state: { data, isReadOnly: false, isUpdate: true } })
        })
    }

    const removeExpence = (e) => {
        deleteExpence(e.currentTarget.id).then(d => {
            if(d === true) {
                setNeedRefresh(!needRefresh)
                saveOperationResult("Gasto eliminado")
            }
            else {
                saveOperationResult("Ocurrio un error")
            }
        })
    }

    const handlePageClick = (e) => {
        const newOffset = e.selected * itemsPerPage % expenceData.length
        setItemOffset(newOffset)
    }

    return(
        <Fragment>
            <Card>
                <Card.Title>Gastos</Card.Title>
                { currentItems === undefined ? "No hay gastos registrados" :
                    <ListGroup variant="flush">
                        {currentItems.map(e => 
                            <ListGroup.Item key={e.id}>
                                <b>{e.name}</b>:${formatAmount(e.amount)} ({(e.expenceType === 1 ? "Base" : "Adicional")})
                                <br />
                                {moment(e.date).format("DD/MM/YYYY")}
                                <div>
                                <Button 
                                    variant="primary" 
                                    size="sm" 
                                    id={ e.id }
                                    onClick={ viewExpence }
                                >
                                    <EyeFill id={e.id}/>
                                </Button>{' '}
                                <Button 
                                    variant="warning" 
                                    size="sm"
                                    id={ e.id }
                                    onClick={ updateExpence }
                                >
                                    <Clipboard2Data/>
                                </Button>{' '}
                                <Button 
                                    variant="danger" 
                                    size="sm"
                                    id={ e.id }
                                    onClick={ removeExpence }
                                >
                                    <Trash2Fill/>
                                </Button>
                            </div>
                            </ListGroup.Item>
                        )}
                    </ListGroup>
                }
                <ReactPaginate
                    nextLabel=">"
                    onPageChange={handlePageClick}
                    pageRangeDisplayed={5}
                    marginPagesDisplayed={2}
                    pageCount={pageCount}
                    previousLabel="<"
                    pageClassName="page-item"
                    pageLinkClassName="page-link"
                    previousClassName="page-item"
                    previousLinkClassName="page-link"
                    nextClassName="page-item"
                    nextLinkClassName="page-link"
                    breakLabel="..."
                    breakClassName="page-item"
                    breakLinkClassName="page-link"
                    containerClassName="pagination"
                    activeClassName="active"
                    renderOnZeroPageCount={null}
                />
            </Card>
        </Fragment>
    )
}

ExpencesViewMobile.propTypes = {
    data: PropTypes.array,
    setExpencesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setExpencesMonth: PropTypes.func,
    isMonthClosedState: PropTypes.bool
};