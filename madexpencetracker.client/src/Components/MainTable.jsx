import { Fragment, useEffect, useState } from "react";
import { ExpenseTable } from "./Tables/ExpenseTable"
import { IncomeTable } from "./Tables/IncomeTable";
import { AmountsTable } from "./Tables/AmountsTable";
import { CalculateAmounts } from "../utils/CalculateAmounts";
import { expences } from "../mocks/expences";
import { incomes } from "../mocks/incomes";
import { Button, Col, Row } from "react-bootstrap";


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

    const closeMonth = () => {
        alert("wea")
    }

    //async function populateWeatherData() {
    //    const response = await fetch('weatherforecast');
    //    const data = await response.json();
    //    setForecasts(data);
    //}



    return (
        <Fragment>
            <Row>
                <Col xs={12}>
                <AmountsTable data={amounts} />
                </Col>
            </Row>
            <Row>
                <Col xs={9}>
                    <ExpenseTable data={expenceData} />
                </Col>
                <Col xs={3}>
                    <IncomeTable data={incomeData} />
                </Col>
            </Row>
            <Row>
                <Button variant="danger" onClick={closeMonth}>Cerrar Mes</Button>
            </Row>
        </Fragment>
    )
}