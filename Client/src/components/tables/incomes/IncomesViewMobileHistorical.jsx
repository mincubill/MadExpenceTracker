/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Card, ListGroup } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { Fragment, useEffect, useState } from "react";
import { deleteIncome, getIncomesById, getIncomeById } from "../../../gateway/incomesGateway";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import { useNavigate } from "react-router-dom";
import ReactPaginate from 'react-paginate';
import { formatAmount } from "../../../utils/numberFormatter";

export const IncomesViewMobileHistorical = ({incomesId, saveOperationResult}) => {
    
    const itemsPerPage = 10
    const [incomeData, setIncomeData] = useState();  
    const [needRefresh, setNeedRefresh] = useState()
    const [currentItems, setCurrentItems] = useState(undefined);
    const [pageCount, setPageCount] = useState(0);
    const [itemOffset, setItemOffset] = useState(0);

    useEffect(() => {
       getIncomesById(incomesId).then(d => {
            if(d.income === undefined) {
                setIncomeData(undefined)
            } else {
                setIncomeData(d.income.reverse())
                setPageCount(Math.ceil(d.income.length / itemsPerPage))
                const endOffset = itemOffset + itemsPerPage
                setCurrentItems(d.income.slice(itemOffset, endOffset))
            }
            
        })
       
    }, [incomesId, needRefresh, itemOffset, itemsPerPage])

    const navigate = useNavigate()

    const viewIncome = (e) => {
        getIncomeById(e.currentTarget.id).then(d => {
            const data = {
                id: d.id,
                name: d.name,
                date: d.date,
                amount: d.amount
            }
            navigate("/income", {state: { data, isReadOnly: true, isUpdate: false } })
        })
    }

    const updateIncome = (e) => {
        getIncomeById(e.currentTarget.id).then(d => {
            const data = {
                id: d.id,
                name: d.name,
                date: d.date,
                amount: d.amount
            }
            navigate("/income", {state: { data, isReadOnly: false, isUpdate: true } })
        })
    }

    const removeIncome = (e) => {
        deleteIncome(e.currentTarget.id).then(d => {
            if(d === true) {
                setNeedRefresh(!needRefresh)
                saveOperationResult("Ingreso eliminado")
            }
            else {
                saveOperationResult("Ocurrio un error")
            }
        })
    }

    const handlePageClick = (e) => {
        const newOffset = e.selected * itemsPerPage % incomeData.length
        setItemOffset(newOffset)
    }

    return (
        <Fragment>
            <Card>
                <Card.Title>Ingresos</Card.Title>
                {currentItems === undefined ? "No hay ingresos registrados" :
                <ListGroup variant="flush">
                    {currentItems.map(e => 
                        <ListGroup.Item key={e.id}>
                            <b>{e.name}</b>: {formatAmount( e.amount )} ({(e.expenceType === 1 ? "Base" : "Adicional")})
                                <br />
                                {moment(e.date).format("DD/MM/YYYY")}
                                <div>
                                    <Button 
                                        variant="primary" 
                                        size="sm" 
                                        id={ e.id }
                                        onClick={ viewIncome }
                                    >
                                        <EyeFill id={e.id}/>
                                    </Button>{' '}
                                    <Button 
                                        variant="warning" 
                                        size="sm"
                                        id={ e.id }
                                        onClick={ updateIncome }
                                    >
                                        <Clipboard2Data/>
                                    </Button>{' '}
                                    <Button 
                                        variant="danger" 
                                        size="sm"
                                        id={ e.id }
                                        onClick={ removeIncome }
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

IncomesViewMobileHistorical.propTypes = {
    incomesId: PropTypes.string,
    saveOperationResult: PropTypes.func,
};