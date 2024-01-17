/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useEffect, useState } from "react";
import { deleteIncome, getCurrentIncomes, getIncomeById } from "../../../gateway/incomesGateway";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import { useNavigate } from "react-router-dom";
import ReactPaginate from 'react-paginate';

export const IncomeTable = ({setIncomesId, saveOperationResult, setIncomesMonth, isMonthClosed}) => {
    
    const itemsPerPage = 10
    const [incomeData, setIncomeData] = useState();  
    const [needRefresh, setNeedRefresh] = useState()
    const [currentItems, setCurrentItems] = useState(undefined);
    const [pageCount, setPageCount] = useState(0);
    const [itemOffset, setItemOffset] = useState(0);

    useEffect(() => {
        getCurrentIncomes().then(d => {
            if(d.expence === undefined || d.expence.length === 0) {
                setIncomeData(undefined)
            }
            setIncomeData(d.income.reverse())
            setIncomesId(d.id)
            setIncomesMonth(d.runningMonth)
            setPageCount(Math.ceil(d.income.length / itemsPerPage));
            const endOffset = itemOffset + itemsPerPage;
            setCurrentItems(d.income.slice(itemOffset, endOffset));
        })
    }, [needRefresh, isMonthClosed, itemOffset, itemsPerPage])

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
        const newOffset = e.selected * itemsPerPage % incomeData.length;
        setItemOffset(newOffset);
    }

    return (
        <>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Fecha</th>
                        <th>Valor</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    { currentItems === undefined || currentItems.length === 0? 
                    <tr>
                        <td colSpan={4}>No hay ingresos registrados</td>
                    </tr> :
                    currentItems.map(d => (
                        <tr key={d.id}>
                            <td>{d.name ? d.name : null}</td>
                            <td>{d.date ? moment(d.date).format("DD/MM/YYYY") : null}</td>
                            <td>{d.amount ? d.amount : null}</td>
                            { d.name ?
                            <td>
                                <span>
                                    <Button 
                                        variant="primary" 
                                        size="sm" 
                                        id={ d.id }
                                        onClick={ viewIncome }
                                    >
                                        <EyeFill id={d.id}/>
                                    </Button>{' '}
                                    <Button 
                                        variant="warning" 
                                        size="sm"
                                        id={ d.id }
                                        onClick={ updateIncome }
                                    >
                                        <Clipboard2Data/>
                                    </Button>{' '}
                                    <Button 
                                        variant="danger" 
                                        size="sm"
                                        id={ d.id }
                                        onClick={ removeIncome }
                                    >
                                        <Trash2Fill/>
                                    </Button>
                                </span>
                            </td> : null}
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

IncomeTable.propTypes = {
    setIncomesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setIncomesMonth: PropTypes.func,
    isMonthClosed: PropTypes.bool
};