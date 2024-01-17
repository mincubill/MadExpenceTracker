/* eslint-disable react-hooks/exhaustive-deps */
import { Fragment, useState } from "react";
import { AmountsTableHistorical } from "./amounts/AmountsTableHistorical";
import { ExpenseTableHistorical } from "./expences/ExpenseTableHistorical";
import { IncomeTableHistorical } from "./incomes/IncomeTableHistorical";
import { Alert, Col, Row } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useMediaQuery } from 'react-responsive'
import { ExpencesViewMobileHistorical } from "./expences/ExpencesViewMobileHistorical";
import { IncomesViewMobileHistorical } from "./incomes/IncomesViewMobileHistorical"
import { AmountsViewMobileHistorical } from "./amounts/AmountsViewMobileHistorical";

export const MainTableHistorical = ({savingsRate, 
    baseExpencesRate, 
    aditionalExpencesRate, 
    amountsId, 
    expencesId, 
    incomesId}) => {    
    const [operationResult, saveOperationResult] = useState(undefined)

    const isDesktopOrLaptop = useMediaQuery({query: '(min-width: 1224px)'})
    const isTabletOrMobile = useMediaQuery({ query: '(max-width: 1224px)' })
    
    return(
        <Fragment>
            {   operationResult === undefined ? null : 
                operationResult ? 
                    <Alert variant="success">{operationResult}</Alert> : 
                    <Alert variant="danger">{operationResult}</Alert>
            }
            <Row>
                {isDesktopOrLaptop ? 
                <AmountsTableHistorical
                    amountsId={amountsId}
                    savingsRate={savingsRate}
                    baseExpencesRate={baseExpencesRate}
                    aditionalExpencesRate={aditionalExpencesRate}
                /> : null }
                {isTabletOrMobile ? 
                    <AmountsViewMobileHistorical 
                        amountsId={amountsId}
                        savingsRate={savingsRate}
                        baseExpencesRate={baseExpencesRate}
                        aditionalExpencesRate={aditionalExpencesRate}
                    /> 
                : null}
            </Row>
            <br />
            {isDesktopOrLaptop ?
                <Row>
                    <Col xs={8}>
                        <ExpenseTableHistorical
                            expencesId={expencesId} 
                            saveOperationResult={saveOperationResult}
                        />
                    </Col>
                    <Col xs={4}>
                        <IncomeTableHistorical
                            incomesId={incomesId} 
                            saveOperationResult={saveOperationResult}
                        />
                    </Col>
                </Row> 
            : null }
            {isTabletOrMobile ? 
                <Row>
                    <ExpencesViewMobileHistorical 
                        expencesId={expencesId} 
                        saveOperationResult={saveOperationResult}
                    />
                </Row>
            : null}
            {isTabletOrMobile ? 
                <Row>
                    <IncomesViewMobileHistorical 
                        incomesId={incomesId} 
                        saveOperationResult={saveOperationResult}
                    />
                </Row>

            : null}
        </Fragment>
    )
}

MainTableHistorical.propTypes = {
    savingsRate: PropTypes.number,
    baseExpencesRate: PropTypes.number,
    aditionalExpencesRate: PropTypes.number,
    amountsId: PropTypes.string,
    expencesId: PropTypes.string,
    incomesId: PropTypes.string,
};