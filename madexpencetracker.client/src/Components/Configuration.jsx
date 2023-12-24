import { Card, Container, Form, Col, Row, Button } from "react-bootstrap"
import PropTypes from 'prop-types';
import { useState } from "react";

export const Configuration = () => {

    const [savingsRate, saveSavingsRate] = useState({})

    const saveConfig = (e) => {
        e.preventDefault()

        //TODO Post config

    }

    return (
        <Card className="p-3">
            <Container>
                <Form onSubmit={saveConfig}>
                    <Form.Group as={Row} className="mb-3">
                        <Form.Label column sm="2">% de ahorro mesual: </Form.Label>
                        <Col sm="10">
                            <Form.Control
                                type="number"
                                placeholder="20"
                                value={savingsRate}
                                onChange={e => saveSavingsRate(e.target.value)}
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

Configuration.propTypes = {
    savingsRate: PropTypes.object,
    saveSavingsRate: PropTypes.object
};