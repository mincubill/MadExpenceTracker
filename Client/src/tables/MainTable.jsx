import { Fragment, useEffect, useState } from "react";
import { ExpenseTable } from "./expences/ExpenseTable";
import { IncomeTable } from "./incomes/IncomeTable";
import { AmountsTable } from "./amounts/AmountsTable";
import { Alert, Col, Row } from "react-bootstrap";
import { getCurrentExpences } from "../gateway/expenceGateway";
import { getCurrentIncomes } from "../gateway/incomesGateway";
import { getCurrentAmounts } from "../gateway/amountsGateway";


export const MainTable = () => {

    const [dataLoaded, setDataLoaded] = useState(false)
    const [expencesId, setExpencesId] = useState('')
    const [incomesId, setIncomesId] = useState('')
    const [expenceData, setExpenceData] = useState([]);   
    const [incomeData, setIncomeData] = useState([]);   
    const [amounts, setAmounts] = useState({});
    const [operationResult, saveOperationResult] = useState(undefined)

    const fecthData = () => {
        getCurrentExpences().then(d => {
            if(d.expence.length === 0) return
            setExpencesId(d.id);
            setExpenceData(d.expence)
        })
        getCurrentIncomes().then(d => {
            if(d.income.length === 0) return
            setIncomesId(d.id)
            setIncomeData(d.income)
        })
        setDataLoaded(true)
    }

    useEffect(() => {
        fecthData()
    }, [])

    useEffect(() => {
        fecthData()
        if(dataLoaded && expencesId !== '' && incomesId !== '') {
            getCurrentAmounts(expencesId, incomesId).then(d => {
                setAmounts(d)
            })
        }
    }, [operationResult, expencesId, incomesId, dataLoaded])

    useEffect(() => {
        if(dataLoaded && expencesId !== '' && incomesId !== '') {
            getCurrentAmounts(expencesId, incomesId).then(d => {
                setAmounts(d)
            })
        }
    }, [expencesId, incomesId, dataLoaded])

    return(
        <Fragment>
            {   operationResult === undefined ? null : 
                operationResult ? 
                    <Alert variant="success">{operationResult}</Alert> : 
                    <Alert variant="danger">{operationResult}</Alert>
            }
            <Row>
                <AmountsTable data={amounts} />
            </Row>
            <Row>
                <Col xs={8}>
                    <ExpenseTable data={expenceData} saveOperationResult={saveOperationResult} />
                </Col>
                <Col xs={4}>
                    <IncomeTable data={incomeData} />
                </Col>
            </Row>
        </Fragment>
        
    )
    
}