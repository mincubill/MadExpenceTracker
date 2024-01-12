/* eslint-disable react-hooks/exhaustive-deps */
import { Fragment, useState } from "react";
import { AmountsTableHistorical } from "./amounts/AmountsTableHistorical";
import { ExpenseTableHistorical } from "./expences/ExpenseTableHistorical";
import { IncomeTableHistorical } from "./incomes/IncomeTableHistorical";
import { Alert, Col, Row } from "react-bootstrap";
import PropTypes from 'prop-types';


export const MainTableHistorical = ({savingsRate, 
    baseExpencesRate, 
    aditionalExpencesRate, 
    amountsId, 
    expencesId, 
    incomesId}) => {    
    const [operationResult, saveOperationResult] = useState(undefined)
    
    return(
        <Fragment>
            
            <Fragment>
                {   operationResult === undefined ? null : 
                    operationResult ? 
                        <Alert variant="success">{operationResult}</Alert> : 
                        <Alert variant="danger">{operationResult}</Alert>
                }
                <Row>
                    <AmountsTableHistorical
                        amountsId={amountsId}
                        savingsRate={savingsRate}
                        baseExpencesRate={baseExpencesRate}
                        aditionalExpencesRate={aditionalExpencesRate}
                    />
                </Row>
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
            </Fragment>
            
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