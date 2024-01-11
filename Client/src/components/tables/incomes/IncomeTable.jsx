/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useEffect, useState } from "react";
import { deleteIncome, getCurrentIncomes, getIncomeById } from "../../../gateway/incomesGateway";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import { useNavigate } from "react-router-dom";

export const IncomeTable = ({setIncomesId, saveOperationResult, setIncomesMonth, isMonthClosed}) => {
    
    
    const [incomeData, setIncomeData] = useState();  
    const [needRefresh, setNeedRefresh] = useState()

    useEffect(() => {
        getCurrentIncomes().then(d => {
            if(d.income.length === 0) {
                setIncomeData(undefined)
            }
            setIncomeData(d.income)
            setIncomesId(d.id)
            setIncomesMonth(d.runningMonth)
        })
    }, [needRefresh, isMonthClosed])

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

    return (
        <Table>
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Fecha</th>
                    <th>Valor</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                { incomeData === undefined ? 
                <tr>
                    <td colSpan={4}>No hay ingresos registrados</td>
                </tr> :
                incomeData.map(d => (
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
    )
}

IncomeTable.propTypes = {
    setIncomesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setIncomesMonth: PropTypes.func,
    isMonthClosed: PropTypes.bool
};