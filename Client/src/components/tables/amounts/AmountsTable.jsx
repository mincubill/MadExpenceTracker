import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';
import { useEffect, useState } from "react";
import { getCurrentAmounts } from "../../../gateway/amountsGateway";
import { getConfiguration } from "../../../gateway/configurationGateway";


export const AmountsTable = ({incomesId, expencesId, operationResult, isMonthClosed}) => {

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

    return (
        <Table responsive>
            <thead>
                <tr>
                    <th></th>
                    <th>Gasto Base({configuration.baseExpencesRate}%)</th>
                    <th>Gasto Adicional({configuration.aditionalExpencesRate}%)</th>
                    <th>Ahorro({configuration.savingsRate}%)</th>
                    <th>Total ingresos</th>
                </tr>
            </thead>
            <tbody>
                {amounts === undefined ? 
                    <tr>
                        <td colSpan={4}>Se necesitan gastos e ingresos registrados para realizar los calculos</td>
                    </tr> : 
                    <tr key={amounts.id}>
                        <td><b>Totales</b></td>
                        <td style={ isExceeded(amounts.sugestedBaseExpences, amounts.totalBaseExpences) ? null : alertExceed }>
                            {amounts.totalBaseExpences}
                        </td>
                        <td style={ isExceeded(amounts.sugestedAditionalExpences, amounts.totalAditionalExpences) ? null : alertExceed }>
                            {amounts.totalAditionalExpences}
                        </td>
                        <td>
                            {amounts.savings}
                        </td>
                        <td>
                            {amounts.totalIncomes}
                        </td>
                    </tr>
                    
                }
                {amounts === undefined ? null : 
                    <tr key={uuidv4()}>
                        <td><b>Sugerido</b></td>
                        <td>{amounts.sugestedBaseExpences}</td>
                        <td>{amounts.sugestedAditionalExpences}</td>
                    </tr> 
                }
                {amounts === undefined ? null : 
                    <tr key={uuidv4()}>
                        <td><b>Restante</b></td>
                        <td>{amounts.remainingBaseExpences}</td>
                        <td>{amounts.remainingAditionalExpences}</td>
                    </tr> 
                }
            </tbody>
        </Table>
    )
}
AmountsTable.propTypes = {
    incomesId: PropTypes.string,
    expencesId: PropTypes.string,
    operationResult: PropTypes.string,
    isMonthClosed: PropTypes.bool
};