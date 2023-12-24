import { useState } from "react"
import { Card, Form, Row, Col, Container, Button } from "react-bootstrap"
import DatePicker from 'react-datepicker'
import moment from "moment";
import { v4 as uuidv4 } from 'uuid';
import "react-datepicker/dist/react-datepicker.css";
import { expences } from "../../mocks/expences";
import { useNavigate } from 'react-router-dom';

export const ExpencesForm = () => {

    const [name, saveName] = useState('')
    const [datePicked, saveDatePicked] = useState(new Date())
    const [expenceType, saveExpenceType] = useState()
    const [amount, saveAmount] = useState(0)
    const navigate = useNavigate();

    const formatDate = (unformatted) => {
        return moment(unformatted).format("YYYY/MM/DD")
    }

    const saveExpence = (e) => {
        e.preventDefault()
        const expenceData = {
            id: uuidv4(),
            name,
            date: formatDate(datePicked),
            expenceType,
            amount
        }
        expences.push(expenceData)
        //TODO Post logica
        navigate("/")
    }

    return (
        <Card className="p-3">
            <Container>
                <Form onSubmit={saveExpence}>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Nombre: </Form.Label>
                        <Col sm="10">
                            <Form.Control
                                type="text"
                                placeholder="Supermercado"
                                value={name}
                                onChange={e => saveName(e.target.value)}
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
                                value="Base"
                                id={`inline-radio-1`}
                                onChange={e => saveExpenceType(e.target.value)}
                            />
                            <Form.Check
                                inline
                                label="adicional"
                                name="expenceType"
                                type="radio"
                                value="Aditional"
                                id={`inline-radio-2`}
                                onChange={e => saveExpenceType(e.target.value)}
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
                                onChange={e => saveAmount(parseInt(e.target.value, 10))}
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