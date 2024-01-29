import { Fragment } from "react"
import { useEffect, useState } from "react";
import { getCurrentAmounts } from "../../../gateway/amountsGateway";
import { getConfiguration } from "../../../gateway/configurationGateway";
import PropTypes from 'prop-types';
import { Card, ListGroup } from "react-bootstrap";
import { formatAmount } from "../../../utils/numberFormatter";


export const AmountsViewMobile = ({incomesId, expencesId, operationResult, isMonthClosed}) => {
    const [amounts, setAmounts] = useState({});
    const [configuration, setConfiguration] = useState({})

    const alertExceed = {
        color: 'whitesmoke',
        backgroundColor: '#e77d7d'
    }

    const alertSavingSuccess = {
        color: 'whitesmoke',
        backgroundColor: '#629443'
    }

    useEffect(() => {
        if(incomesId !== '' && expencesId !== '') {
            getCurrentAmounts(expencesId, incomesId).then(d => {
                setAmounts(d)
            })
            getConfiguration().then(d => {
                setConfiguration(d)
            })
        }
        else {
            setAmounts(undefined)
        }
    }, [expencesId, incomesId, operationResult, isMonthClosed])

    const isExceeded = (sugested, total) => {
        return sugested >= total
    }

    return(
        <Fragment>
            <Card>
                <Card.Title>Totales</Card.Title>
                {amounts ?
                <ListGroup variant="flush">
                    <ListGroup.Item>
                        <b>Ingresos</b>: {formatAmount(amounts.totalIncomes)}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.savings, amounts.totalSavings) ? null : alertSavingSuccess }>
                        <b>Ahorro sugerido({configuration.savingsRate}%)</b>: {formatAmount(amounts.savings)}
                        <br/>
                        <b>Ahorro total</b>: {formatAmount(amounts.totalSavings)}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedBaseExpences, amounts.totalBaseExpences) ? null : alertExceed }>
                        <b>Gastos Base({configuration.baseExpencesRate}%)</b>: {formatAmount(amounts.totalBaseExpences)}
                        <br/>
                        <b>Sugerido</b>: {formatAmount(amounts.sugestedBaseExpences)}
                        <br />
                        <b>Restante</b>: {formatAmount(amounts.remainingBaseExpences)}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedAditionalExpences, amounts.totalAditionalExpences) ? null : alertExceed }>
                        <b>Gastos adicionales({configuration.totalAditionalExpences}%)</b>: {formatAmount(amounts.totalBaseExpences)}
                        <br/>
                        <b>Sugerido</b>: {formatAmount(amounts.sugestedAditionalExpences)}
                        <br/>
                        <b>Restante</b>: {formatAmount(amounts.remainingAditionalExpences)}
                    </ListGroup.Item>
                </ListGroup> : "Se necesitan gastos e ingresos registrados para realizar los calculos"}
            </Card>
        </Fragment>
    )

}

AmountsViewMobile.propTypes = {
    incomesId: PropTypes.string,
    expencesId: PropTypes.string,
    operationResult: PropTypes.string,
    isMonthClosed: PropTypes.bool
};