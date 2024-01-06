import { useState } from "react"
import { Card, Form, Row, Col, Container, Button } from "react-bootstrap"
import { useNavigate } from "react-router-dom"
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";
import moment from "moment";
import { v4 as uuidv4 } from 'uuid';
import { incomes } from "./mocks/incomes";

export const IncomeForm = () => {

    const [name, saveName] = useState('')
    const[datePicked, saveDatePicked] = useState(new Date())
    const[amount, saveAmount] = useState(0)
    const navigate = useNavigate();

    const formatDate = (unformatted) => {
        return moment(unformatted).format("YYYY/MM/DD")
    }

    const saveIncome = (e) => {
        e.preventDefault()
        const incomeData = {
            id: uuidv4(),
            name,
            date: formatDate(datePicked),
            amount
        }  
        incomes.push(incomeData)
        //TODO Post logica
        navigate("/")
    }

    return (
        <Card className="p-3">
            <Container>
                <Form onSubmit={saveIncome}>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">Nombre: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="text" 
                            placeholder="Sueldo" 
                            value={name}
                            onChange={e => saveName(e.target.value)}
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