/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useNavigate } from "react-router-dom";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import 'bootstrap/dist/css/bootstrap.min.css'
import { deleteExpence, getCurrentExpences, getExpenceById } from "../../../gateway/expenceGateway";
import { useEffect, useState } from "react";

export const ExpenseTable = ({setExpencesId, saveOperationResult, setExpencesMonth, isMonthClosedState}) => {

    const [expenceData, setExpenceData] = useState([])
    const [needRefresh, setNeedRefresh] = useState(false)

    useEffect(() => {

        getCurrentExpences().then(d => {
            if(d.expence === undefined || d.expence.length === 0) {
                setExpenceData(undefined)
                return
            }
            setExpenceData(d.expence)
            setExpencesId(d.id)
            setExpencesMonth(d.runningMonth)
        })
    }, [isMonthClosedState, needRefresh])

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

    return (
        <Table>
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
                
                { expenceData === undefined ? 
                <tr>
                    <td colSpan={5}>No hay gastos registrados</td>
                </tr> :
                expenceData.map(d => ( 
                    <tr key={d.id}>
                        {!d.name ? <td colSpan={5}>Sin gastos registrados</td> : null}
                        <td>{d.name}</td>
                        <td>{moment(d.date).format("DD/MM/YYYY")}</td>
                        <td>{(d.expenceType === 1 ? "Base" : "Adicional")}</td>
                        <td>{d.amount }</td>
                         
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
    )
}

ExpenseTable.propTypes = {
    data: PropTypes.array,
    setExpencesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setExpencesMonth: PropTypes.func,
    isMonthClosedState: PropTypes.bool
};