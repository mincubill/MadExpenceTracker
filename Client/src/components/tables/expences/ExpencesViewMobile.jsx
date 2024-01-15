/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Card, ListGroup } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useNavigate } from "react-router-dom";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import 'bootstrap/dist/css/bootstrap.min.css'
import { deleteExpence, getCurrentExpences, getExpenceById } from "../../../gateway/expenceGateway";
import { Fragment, useEffect, useState } from "react";

export const ExpencesViewMobile = ({setExpencesId, saveOperationResult, setExpencesMonth, isMonthClosedState}) => {
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

    return(
        <Fragment>
            <Card>
                <Card.Title>Gastos</Card.Title>
                { expenceData === undefined ? "No hay gastos registrados" :
                    <ListGroup variant="flush">
                        {expenceData.map(e => 
                            <ListGroup.Item key={e.id}>
                                <b>{e.name}</b>:${e.amount} ({(e.expenceType === 1 ? "Base" : "Adicional")})
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