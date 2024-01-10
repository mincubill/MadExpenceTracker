/* eslint-disable react-hooks/exhaustive-deps */
import { Fragment, useEffect, useState } from "react";
import { ExpenseTable } from "./expences/ExpenseTable";
import { IncomeTable } from "./incomes/IncomeTable";
import { AmountsTable } from "./amounts/AmountsTable";
import { Alert, Col, Row } from "react-bootstrap";
import { getConfiguration } from "../../gateway/configurationGateway";
import { useNavigate } from "react-router-dom";


export const MainTable = () => {   
    const [expencesId, setExpencesId] = useState('')
    const [incomesId, setIncomesId] = useState('')  
    const [operationResult, saveOperationResult] = useState(undefined)
    const [configured, setConfigured] = useState(false)

    const navigate = useNavigate()

    useEffect(() => {
        getConfiguration().then((d) => {
            console.log(d)
            if(!d.savingsRate) {
                setConfigured(false)
                navigate("/configuration", { state: {isConfigured: false} })
            }
            setConfigured(true)
        })
    }, [])

    return(
        <Fragment>
            { configured === false ? null : 
            <Fragment>
                {   operationResult === undefined ? null : 
                    operationResult ? 
                        <Alert variant="success">{operationResult}</Alert> : 
                        <Alert variant="danger">{operationResult}</Alert>
                }
                <Row>
                    <AmountsTable 
                        incomesId={incomesId}
                        expencesId={expencesId} 
                        operationResult={operationResult}
                    />
                </Row>
                <Row>
                    <Col xs={8}>
                        <ExpenseTable setExpencesId={setExpencesId} saveOperationResult={saveOperationResult} />
                    </Col>
                    <Col xs={4}>
                        <IncomeTable setIncomesId={setIncomesId} saveOperationResult={saveOperationResult} />
                    </Col>
                </Row>
            </Fragment>
            }
            
            
        </Fragment>
        
    )
    
}