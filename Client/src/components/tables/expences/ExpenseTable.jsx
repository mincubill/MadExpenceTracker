/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useNavigate } from "react-router-dom";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import 'bootstrap/dist/css/bootstrap.min.css'
import { deleteExpence, getCurrentExpences, getExpenceById } from "../../../gateway/expenceGateway";
import { useEffect, useState } from "react";
import ReactPaginate from 'react-paginate';
import { formatAmount } from "../../../utils/numberFormatter";
import { getExpenceType } from "../../../utils/expenceType";

export const ExpenseTable = ({setExpencesId, saveOperationResult, setExpencesMonth, isMonthClosed}) => {

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
            }
            setExpenceData(d.expence.reverse())
            setExpencesId(d.id)
            setExpencesMonth(d.runningMonth)
            setPageCount(Math.ceil(d.expence.length / itemsPerPage));
            const endOffset = itemOffset + itemsPerPage;
            setCurrentItems(d.expence.slice(itemOffset, endOffset));
        })
    }, [needRefresh, isMonthClosed, itemOffset, itemsPerPage])

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
        const newOffset = e.selected * itemsPerPage % expenceData.length;
        setItemOffset(newOffset);
    }

    return (
        <>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Fecha</th>
                        <th>Tipo</th>
                        <th>Valor</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    
                    { currentItems === undefined || currentItems.length === 0? 
                    <tr>
                        <td colSpan={5}>No hay gastos registrados</td>
                    </tr> :
                    currentItems.map(d => ( 
                        <tr key={d.id}>
                            {!d.name ? <td colSpan={5}>Sin gastos registrados</td> : null}
                            <td>{d.name}</td>
                            <td>{moment(d.date).format("DD/MM/YYYY")}</td>
                            <td>{getExpenceType(d.expenceType)}</td>
                            <td>{formatAmount( d.amount )}</td>
                            
                            <td>
                                <span>
                                    <Button 
                                        variant="primary" 
                                        size="sm" 
                                        id={ d.id }
                                        onClick={ viewExpence }
                                    >
                                        <EyeFill id={d.id}/>
                                    </Button>{' '}
                                    <Button 
                                        variant="warning" 
                                        size="sm"
                                        id={ d.id }
                                        onClick={ updateExpence }
                                    >
                                        <Clipboard2Data/>
                                    </Button>{' '}
                                    <Button 
                                        variant="danger" 
                                        size="sm"
                                        id={ d.id }
                                        onClick={ removeExpence }
                                    >
                                        <Trash2Fill/>
                                    </Button>
                                </span>
                            </td>
                        </tr>
                    ))}

                </tbody>
            </Table>
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
        </>
        
    )
}

ExpenseTable.propTypes = {
    data: PropTypes.array,
    setExpencesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setExpencesMonth: PropTypes.func,
    isMonthClosed: PropTypes.bool
};