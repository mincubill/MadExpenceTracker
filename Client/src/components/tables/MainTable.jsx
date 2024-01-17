/* eslint-disable react-hooks/exhaustive-deps */
import { Fragment, useEffect, useState } from "react";
import { ExpenseTable } from "./expences/ExpenseTable";
import { IncomeTable } from "./incomes/IncomeTable";
import { AmountsTable } from "./amounts/AmountsTable";
import { Alert, Col, Row } from "react-bootstrap";
import { getConfiguration } from "../../gateway/configurationGateway";
import { useNavigate } from "react-router-dom";
import PropTypes from 'prop-types';
import { useMediaQuery } from 'react-responsive'
import { AmountsViewMobile } from "./amounts/AmountsViewMobile";
import { ExpencesViewMobile } from "./expences/ExpencesViewMobile";
import { IncomesViewMobile } from "./incomes/IncomesViewMobile";

export const MainTable = ({isMonthClosed}) => {   
    const [expencesId, setExpencesId] = useState('')
    const [incomesId, setIncomesId] = useState('')  
    const [operationResult, saveOperationResult] = useState(undefined)
    const [configured, setConfigured] = useState(false)
    const [expencesMonth, setExpencesMonth] = useState('')
    const [incomesMonth, setIncomesMonth] = useState('')
    
    const navigate = useNavigate()

    useEffect(() => {
        getConfiguration().then((d) => {
            if(!d.savingsRate) {
                setConfigured(false)
                navigate("/configuration", { state: {isConfigured: false} })
            }
            setConfigured(true)
        })
    }, [])

    useEffect(() => {
        if(expencesMonth !== '' && incomesMonth !== '') {
            if(expencesMonth === incomesMonth) {
                localStorage.setItem("monthToClose" ,expencesMonth)
                localStorage.setItem("expencesId" ,expencesId)
                localStorage.setItem("incomesId" ,incomesId)
            }
        }
    }, [expencesMonth, incomesMonth])

    const isDesktopOrLaptop = useMediaQuery({query: '(min-width: 1224px)'})
    const isTabletOrMobile = useMediaQuery({ query: '(max-width: 1224px)' })

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
                        {isDesktopOrLaptop ? 
                        <AmountsTable 
                            incomesId={incomesId}
                            expencesId={expencesId} 
                            operationResult={operationResult}
                            isMonthClosed={isMonthClosed}
                        /> : null}
                        {isTabletOrMobile? 
                            <AmountsViewMobile
                            incomesId={incomesId}
                            expencesId={expencesId} 
                            operationResult={operationResult}
                            isMonthClosed={isMonthClosed}
                        /> : null}
                    </Row>
                    <br />
                    {isDesktopOrLaptop ? <Row>
                        <Col xs={7}>
                            <ExpenseTable 
                                setExpencesId={setExpencesId} 
                                saveOperationResult={saveOperationResult}
                                setExpencesMonth={setExpencesMonth}
                                isMonthClosed={isMonthClosed}
                            />
                        </Col>
                        <Col xs={5}>
                            <IncomeTable 
                                setIncomesId={setIncomesId} 
                                saveOperationResult={saveOperationResult}
                                setIncomesMonth={setIncomesMonth}
                                isMonthClosed={isMonthClosed}
                            />
                        </Col>
                    </Row> : null }
                    <br />
                    
                    {isTabletOrMobile ? 
                    <Row>
                        <ExpencesViewMobile 
                            setExpencesId={setExpencesId} 
                            saveOperationResult={saveOperationResult}
                            setExpencesMonth={setExpencesMonth}
                            isMonthClosedState={isMonthClosed}
                        />
                    </Row> : null}
                    <br />
                    {isTabletOrMobile ? 
                    <Row>
                        <IncomesViewMobile 
                            setIncomesId={setIncomesId} 
                            saveOperationResult={saveOperationResult}
                            setIncomesMonth={setIncomesMonth}
                            isMonthClosed={isMonthClosed}
                        />
                    </Row> : null}
                    
                </Fragment>
            }
        </Fragment>
    )
}

MainTable.propTypes = {
    isMonthClosed: PropTypes.bool
};