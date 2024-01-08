import { useEffect, useState } from "react"
import { Card, Form, Row, Col, Container, Button, Alert } from "react-bootstrap"
import DatePicker from 'react-datepicker'
import moment from "moment";
import { v4 as uuidv4 } from 'uuid';
import "react-datepicker/dist/react-datepicker.css";
import { postExpence } from '../gateway/expenceGateway'
import { useLocation } from "react-router-dom";


export const ExpencesForm = () => {

    
    const[expenceId, saveExpenceId] = useState('')
    const[name, saveName] = useState('')
    const[datePicked, saveDatePicked] = useState(new Date())
    const[expenceType, saveExpenceType] = useState(0)
    const[amount, saveAmount] = useState(0)
    const[success, saveSuccess] = useState(undefined)
    const[isReadOnlyField, saveIsReadOnlyField] = useState(false)

    const location = useLocation()

    useEffect(() => {
        if(!location.state) return;
        const {data ,isReadOnly} = location.state
        saveIsReadOnlyField(isReadOnly)
        saveExpenceId(data.id)
        saveName(data.name)
        saveDatePicked(new Date(data.date))
        saveExpenceType(data.expenceType)
        saveAmount(data.amount)
        console.log(data)
        
    }, [location.state])

    const clearForm = () => {
        saveName('')
        saveDatePicked(new Date())
        saveExpenceType(0)
        saveAmount(0)
    }

    const saveExpence = (e) => {
        e.preventDefault()
        const expenceData = {
            id: uuidv4(),
            name,
            date: moment(datePicked).toJSON(),
            expenceType: parseInt(expenceType),
            amount
        } 
        postExpence(expenceData).then(
            () => {
                saveSuccess(true);
            }
        ).catch(e => {
                saveSuccess(false);
                console.error(e)
            }
        )
        clearForm()
    }

    const updateExpence = (e) => {
        //TODO logica de udpate
    }

   return (
        <Card className="p-3">
            <Container>
                { success === undefined ? null : 
                    success ? 
                        <Alert variant="success">Guardado</Alert> : 
                        <Alert variant="danger">Ocurrio un error</Alert>
                }
                <Form onSubmit={saveExpence}>
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
                    <Button variant="primary" type="Submit">
                        Guardar
                    </Button>
                </Form>
            </Container>
        </Card>
    )
}
