import { useEffect, useState } from "react"
import { Alert, Button, Card, Col, Container, Form, Row } from "react-bootstrap"
import { getConfiguration, postConfiguration, updateConfiguration } from "../../gateway/configurationGateway"
import { useLocation } from "react-router-dom"

export const ConfigurationForm = () => {
    const [savingsRate, setSavingsRate] = useState(0)
    const [aditionalExpencesRate, setAditionalExpencesRate] = useState(0)
    const [baseExpencesRate, setBaseExpencesRate] = useState(0)
    const [operationResult, setOperationResult] = useState()
    const [resultMessage, setResultMessage] = useState()
    const [isConfigured, setIsConfigured] = useState(false)

    const location = useLocation()
    
    useEffect(() => {
        const {isConfigured} = location.state
            if(isConfigured === false){
            setIsConfigured(false)
        }
        else {
            setIsConfigured(true)
            getConfiguration().then(d => {
                setBaseExpencesRate(d.baseExpencesRate)
                setAditionalExpencesRate(d.aditionalExpencesRate)
                setSavingsRate(d.savingsRate)
            })
        }
    }, [isConfigured, location])

    const saveConfiguration = (e) => {
        e.preventDefault()
        const configuration = {
            savingsRate: savingsRate,
            baseExpencesRate: baseExpencesRate,
            aditionalExpencesRate: aditionalExpencesRate
        }
        if(isConfigured) {
            updateConfiguration(configuration).then(() => {
                setOperationResult(true);
                setResultMessage("Configuracion guardado")
            }).catch(e => {
                setOperationResult(false);
                setResultMessage("Ocurrio un error")
                console.error(e)
            })
        } else {
            postConfiguration(configuration).then(() => {
                setOperationResult(true);
                setResultMessage("Configuracion guardado")
            }).catch(e => {
                setOperationResult(false);
                setResultMessage("Ocurrio un error")
                console.error(e)
            })
        }
        
    }


    return (
        <Card className="p-3">
            <Container>
                {!isConfigured ? <Alert variant="danger">No se detecto configuracion</Alert> : null}
                { operationResult === undefined ? null : 
                    operationResult ? 
                        <Alert variant="success">{resultMessage}</Alert> : 
                        <Alert variant="danger">{resultMessage}</Alert>
                }
                <Form onSubmit={saveConfiguration}>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">% Ahorro: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="number" 
                            placeholder="% Ahorro" 
                            value={savingsRate}
                            onChange={e => setSavingsRate( parseInt( e.target.value, 10 ) )}
                            required/>
                        </Col>
                    </Form.Group>
                    
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">% Gasto base: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="number" 
                            placeholder="% Gasto base"
                            value={baseExpencesRate}
                            onChange={e => setBaseExpencesRate( parseInt( e.target.value, 10 ) )}
                            required />
                        </Col>
                    </Form.Group>
                    
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">% Gasto adicional: </Form.Label>
                        <Col sm="10">
                            <Form.Control 
                            type="number" 
                            placeholder="% Gasto adicional"
                            value={aditionalExpencesRate}
                            onChange={e => setAditionalExpencesRate( parseInt( e.target.value, 10 ) )}
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