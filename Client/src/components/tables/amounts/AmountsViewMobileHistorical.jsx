import { Fragment } from "react"
import { useEffect, useState } from "react";
import { getAmountById } from "../../../gateway/amountsGateway";
import PropTypes from 'prop-types';
import { Card, ListGroup } from "react-bootstrap";
import { formatAmount } from "../../../utils/numberFormatter";


export const AmountsViewMobileHistorical = ({amountsId, savingsRate, baseExpencesRate, aditionalExpencesRate}) => {
    
    const [amounts, setAmounts] = useState({});

    const alertExceed = {
        color: 'whitesmoke',
        backgroundColor: '#e77d7d'
    }

    const alertSavingSuccess = {
        color: 'whitesmoke',
        backgroundColor: '#629443'
    }

    useEffect(() => { 
            if(amountsId === '') {
                setAmounts(undefined)
            }
            else {
                getAmountById(amountsId).then(d => {
                    setAmounts(d)
                })
            }
            
    }, [amountsId])

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
                        <b>Ahorro sugerido({savingsRate}%)</b>: {formatAmount(amounts.savings)}
                        <br/>
                        <b>Ahorro total</b>: {formatAmount(amounts.totalSavings)}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedBaseExpences, amounts.totalBaseExpences) ? null : alertExceed }>
                        <b>Gastos Base({baseExpencesRate}%)</b>: {formatAmount(amounts.totalBaseExpences)}
                        <br/>
                        <b>Sugerido</b>: {formatAmount(amounts.sugestedBaseExpences)}
                        <br />
                        <b>Restante</b>: {formatAmount(amounts.remainingBaseExpences)}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedAditionalExpences, amounts.totalAditionalExpences) ? null : alertExceed }>
                        <b>Gastos adicionales({aditionalExpencesRate}%)</b>: {formatAmount(amounts.totalBaseExpences)}
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

AmountsViewMobileHistorical.propTypes = {
    amountsId: PropTypes.string,
    savingsRate: PropTypes.number,
    baseExpencesRate: PropTypes.number,
    aditionalExpencesRate: PropTypes.number,
};