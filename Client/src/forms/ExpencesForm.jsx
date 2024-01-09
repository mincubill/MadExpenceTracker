import { useEffect, useState } from "react"
import { Card, Form, Row, Col, Container, Button, Alert } from "react-bootstrap"
import DatePicker from 'react-datepicker'
import moment from "moment";
import { v4 as uuidv4 } from 'uuid';
import "react-datepicker/dist/react-datepicker.css";
import { postExpence, updateExpence } from '../gateway/expenceGateway'
import { useLocation } from "react-router-dom";


export const ExpencesForm = () => {

    
    const[expenceId, saveExpenceId] = useState('')
    const[name, saveName] = useState('')
    const[datePicked, saveDatePicked] = useState(new Date())
    const[expenceType, saveExpenceType] = useState(1)
    const[amount, saveAmount] = useState(0)
    const[successResult, saveSuccessResult] = useState(undefined)
    const[isReadOnlyField, saveIsReadOnlyField] = useState(false)
    const[isAnUpdate, saveIsAnUpdate] = useState(false)

    const location = useLocation()

    useEffect(() => {
        if(!location.state) return;
        const {data, isReadOnly, isUpdate} = location.state
        saveIsReadOnlyField(isReadOnly)
        saveIsAnUpdate(isUpdate)
        saveExpenceId(data.id)
        saveName(data.name)
        saveDatePicked(new Date(data.date))
        saveExpenceType(data.expenceType)
        saveAmount(data.amount)
        
    }, [location.state])

    const clearForm = () => {
        saveName('')
        saveDatePicked(new Date())
        saveExpenceType(0)
        saveAmount(0)
    }

    const saveExpence = (e) => {
        e.preventDefault()
        if(!isAnUpdate) {
            const expenceData = {
                id: uuidv4(),
                name,
                date: moment(datePicked).toJSON(),
                expenceType: parseInt(expenceType),
                amount
            } 
            postExpence(expenceData).then(
                () => {
                    saveSuccessResult("gasto guardado");
                }
            ).catch(e => {
                    saveSuccessResult("ocurrio un error");
                    console.error(e)
                }
            )
            clearForm()
        } else {
            const expenceUpdateData = {
                id: expenceId,
                name,
                date: moment(datePicked).toJSON(),
                expenceType: parseInt(expenceType),
                amount
            }
            updateExpence(expenceUpdateData).then(
                () => {
                    saveSuccessResult("gasto actualizado");
                }
            ).catch(e => {
                    saveSuccessResult("ocurrio un error");
                    console.error(e)
                }
            )
        }
    }

   return (
        <Card className="p-3">
            <Container>
                { successResult === undefined ? null : 
                    successResult ? 
                        <Alert variant="success">{successResult}</Alert> : 
                        <Alert variant="danger">{successResult}</Alert>
                }
                <Form onSubmit={ saveExpence }>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Nombre: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="text" 
                            placeholder="Supermercado" 
                            value={name}
                            onChange={e => saveName(e.target.value)}
                            readOnly={isReadOnlyField}
                            required />
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
                        <Form.Label column sm="2">Tipo de gasto: </Form.Label>
                        <Col sm="10">
                            <Form.Check
                                inline
                                label="base"
                                name="expenceType"
                                type="radio"
                                value={1}
                                id={`inline-radio-1`}
                                checked={expenceType == 1}
                                onChange={e => saveExpenceType(e.target.value)}
                                readOnly={isReadOnlyField}
                            />
                            <Form.Check
                                inline  
                                label="adicional"
                                name="expenceType"
                                type="radio"
                                value={2}
                                id={`inline-radio-2`}
                                checked={expenceType == 2}
                                onChange={e => saveExpenceType(e.target.value)}
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
