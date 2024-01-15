import { Fragment } from "react"
import { useEffect, useState } from "react";
import { getCurrentAmounts } from "../../../gateway/amountsGateway";
import { getConfiguration } from "../../../gateway/configurationGateway";
import PropTypes from 'prop-types';
import { Card, ListGroup } from "react-bootstrap";


export const AmountsViewMobile = ({incomesId, expencesId, operationResult, isMonthClosed}) => {
    const [amounts, setAmounts] = useState({});
    const [configuration, setConfiguration] = useState({})

    const alertExceed = {
        color: 'whitesmoke',
        backgroundColor: '#e77d7d'
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
                        <b>Ingresos</b>: {amounts.totalIncomes}
                    </ListGroup.Item>
                    <ListGroup.Item>
                        <b>Ahorro sugerido({configuration.savingsRate}%)</b>: {amounts.savings}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedBaseExpences, amounts.totalBaseExpences) ? null : alertExceed }>
                        <b>Gastos Base({configuration.baseExpencesRate}%)</b>: {amounts.totalBaseExpences}
                        <br/>
                        <b>Sugerido</b>: {amounts.sugestedBaseExpences}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedAditionalExpences, amounts.totalAditionalExpences) ? null : alertExceed }>
                        <b>Gastos adicionales({configuration.totalAditionalExpences}%)</b>: {amounts.totalBaseExpences}
                        <br/>
                        <b>Sugerido</b>: {amounts.sugestedAditionalExpences}
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