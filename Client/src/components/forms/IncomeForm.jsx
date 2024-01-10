import { useEffect, useState } from "react"
import { Card, Form, Row, Col, Container, Button, Alert } from "react-bootstrap"
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";
import moment from "moment";
import { v4 as uuidv4 } from 'uuid';
import { useLocation } from "react-router-dom";
import { updateIncome ,postIncome } from "../../gateway/incomesGateway";

export const IncomeForm = () => {

    const[incomeId, saveIncomeId] = useState('')
    const[name, saveName] = useState('')
    const[datePicked, saveDatePicked] = useState(new Date())
    const[amount, saveAmount] = useState(0)
    const[operationResult, saveOperationResult] = useState(undefined)
    const[resultMessage, saveResultMessage] = useState(undefined)
    const[isReadOnlyField, saveIsReadOnlyField] = useState(false)
    const[isAnUpdate, saveIsAnUpdate] = useState(false)

    const location = useLocation()

    useEffect(() => {
        if(!location.state) return;
        const {data, isReadOnly, isUpdate} = location.state
        saveIsReadOnlyField(isReadOnly)
        saveIsAnUpdate(isUpdate)
        saveIncomeId(data.id)
        saveName(data.name)
        saveDatePicked(new Date(data.date))
        saveAmount(data.amount)
    }, [location.state])

    const clearForm = () => {
        saveName('')
        saveDatePicked(new Date())
        saveAmount(0)
    }

    const saveIncome = (e) => {
        e.preventDefault()
        if(!isAnUpdate){
            const incomeData = {
                id: uuidv4(),
                name,
                date: moment(datePicked).toJSON(),
                amount
            }  
            postIncome(incomeData).then(
                () => {
                    saveOperationResult(true);
                    saveResultMessage("Ingreso guardado")
                }
            ).catch(e => {
                    saveOperationResult(false);
                    saveResultMessage("Ocurrio un error")
                    console.error(e)
                }
            )
        } else {
            const incomeData = {
                id: incomeId,
                name,
                date: moment(datePicked).toJSON(),
                amount
            }  
            updateIncome(incomeData).then(
                () => {
                    saveOperationResult(true);
                    saveResultMessage("Ingreso actualizado")
                }
            ).catch(e => {
                    saveOperationResult(false);
                    saveResultMessage("Ocurrio un error")
                    console.error(e)
                }
            )
        }
        
        clearForm();
    }



    return (
        <Card className="p-3">
            <Container>
                { operationResult === undefined ? null : 
                    operationResult ? 
                        <Alert variant="success">{resultMessage}</Alert> : 
                        <Alert variant="danger">{resultMessage}</Alert>
                }
                <Form onSubmit={saveIncome}>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Nombre: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="text" 
                            placeholder="Sueldo" 
                            value={name}
                            onChange={e => saveName(e.target.value)}
                            readOnly={isReadOnlyField}
                            required/>
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Fecha: </Form.Label>
                        <Col sm="10">
                            <DatePicker
                                dateFormat="dd/MM/yyyy"
                                selected={datePicked}
                                onSelect={(selection) => {
                                    saveDatePicked(selection)
                                }}
                                onChange={(selection) => {
                                    saveDatePicked(selection)
                                }} 
                                readOnly={isReadOnlyField}
                            />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Monto: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="number" 
                            placeholder="10000"
                            value={amount}
                            onChange={e => saveAmount( parseInt( e.target.value, 10 ) )}
                            readOnly={isReadOnlyField}
                            required />
                        </Col>
                    </Form.Group>
                    {isReadOnlyField ? 
                        null : 
                        <Button variant="primary" type="Submit">
                            { isAnUpdate ? "Actualizar" : "Guardar" } 
                        </Button>
                    }
                </Form>
            </Container>
        </Card>
        
    )
}