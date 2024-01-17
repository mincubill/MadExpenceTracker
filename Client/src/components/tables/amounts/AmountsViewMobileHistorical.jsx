import { Fragment } from "react"
import { useEffect, useState } from "react";
import { getAmountById } from "../../../gateway/amountsGateway";
import PropTypes from 'prop-types';
import { Card, ListGroup } from "react-bootstrap";


export const AmountsViewMobileHistorical = ({amountsId, savingsRate, baseExpencesRate, aditionalExpencesRate}) => {
    
    const [amounts, setAmounts] = useState({});

    const alertExceed = {
        color: 'whitesmoke',
        backgroundColor: '#e77d7d'
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
                        <b>Ingresos</b>: {amounts.totalIncomes}
                    </ListGroup.Item>
                    <ListGroup.Item>
                        <b>Ahorro sugerido({savingsRate}%)</b>: {amounts.savings}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedBaseExpences, amounts.totalBaseExpences) ? null : alertExceed }>
                        <b>Gastos Base({baseExpencesRate}%)</b>: {amounts.totalBaseExpences}
                        <br/>
                        <b>Sugerido</b>: {amounts.sugestedBaseExpences}
                    </ListGroup.Item>
                    <ListGroup.Item style={ isExceeded(amounts.sugestedAditionalExpences, amounts.totalAditionalExpences) ? null : alertExceed }>
                        <b>Gastos adicionales({aditionalExpencesRate}%)</b>: {amounts.totalBaseExpences}
                        <br/>
                        <b>Sugerido</b>: {amounts.sugestedAditionalExpences}
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