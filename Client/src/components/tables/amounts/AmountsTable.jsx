import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';
import { useEffect, useState } from "react";
import { getCurrentAmounts } from "../../../gateway/amountsGateway";
import { getConfiguration } from "../../../gateway/configurationGateway";


export const AmountsTable = ({incomesId, expencesId, operationResult}) => {

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
        
    }, [expencesId, incomesId, operationResult])

    const isExceeded = (sugested, total) => {
        return sugested >= total
    }

    return (
        <Table>
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
                <tr key={uuidv4()}>
                    <td><b>Sugerido</b></td>
                    <td>{amounts.sugestedBaseExpences}</td>
                    <td>{amounts.sugestedAditionalExpences}</td>
                </tr>
            </tbody>
        </Table>
    )
}
AmountsTable.propTypes = {
    incomesId: PropTypes.string,
    expencesId: PropTypes.string,
    operationResult: PropTypes.string

};