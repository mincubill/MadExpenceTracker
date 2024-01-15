/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Card, ListGroup } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { Fragment, useEffect, useState } from "react";
import { deleteIncome, getCurrentIncomes, getIncomeById } from "../../../gateway/incomesGateway";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import { useNavigate } from "react-router-dom";

export const IncomesViewMobile = ({setIncomesId, saveOperationResult, setIncomesMonth, isMonthClosed}) => {
    
    const [incomeData, setIncomeData] = useState();  
    const [needRefresh, setNeedRefresh] = useState()

    useEffect(() => {
        getCurrentIncomes().then(d => {
            if(d.expence === undefined || d.expence.length === 0) {
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
        <Fragment>
            <Card>
                <Card.Title>Ingresos</Card.Title>
                {incomeData === undefined ? "No hay ingresos registrados" :
                <ListGroup variant="flush">
                    {incomeData.map(e => 
                        <ListGroup.Item key={e.id}>
                            <b>{e.name}</b>:${e.amount} ({(e.expenceType === 1 ? "Base" : "Adicional")})
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
                </ListGroup>}
            </Card>
        </Fragment>
    )
}

IncomesViewMobile.propTypes = {
    setIncomesId: PropTypes.func,
    saveOperationResult: PropTypes.func,
    setIncomesMonth: PropTypes.func,
    isMonthClosed: PropTypes.bool
};