import { Fragment, useEffect, useState } from "react";
import { ExpenseTable } from "./ExpenseTable";
import { IncomeTable } from "./IncomeTable";
import { CalculateAmounts } from "./utils/CalculateAmounts";
import { AmountsTable } from "./AmountsTable";
import { expences } from "./mocks/expences";
import { incomes } from "./mocks/incomes";
import { Col, Row } from "react-bootstrap";


export const MainTable = () => {

    const [expenceData, setExpenceData] = useState([]);   
    const [incomeData, setIncomeData] = useState([]);   
    const [amounts, setAmounts] = useState({});

    useEffect(() => {
        //TODO Fetch de data
        setExpenceData(expences)
        setIncomeData(incomes)
    }, [])

    useEffect(() => {
        setAmounts(CalculateAmounts(expenceData, incomeData))
    }, [expenceData, incomeData])

    return(
        <Fragment>
            <Row>
                <AmountsTable data={amounts} />
            </Row>
            <Row>
                <Col xs={9}>
                    <ExpenseTable data={expenceData} />
                </Col>
                <Col xs={3}>
                    <IncomeTable data={incomeData} />
                </Col>
            </Row>
        </Fragment>
        
    )
    
}