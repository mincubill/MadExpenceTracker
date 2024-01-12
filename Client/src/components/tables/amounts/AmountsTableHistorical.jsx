import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import { v4 as uuidv4 } from 'uuid';
import { useEffect, useState } from "react";
import { getAmountById } from "../../../gateway/amountsGateway";


export const AmountsTableHistorical = ({amountsId, savingsRate, baseExpencesRate, aditionalExpencesRate}) => {

    const [amounts, setAmounts] = useState({});

    const alertExceed = {
        color: 'whitesmoke',
        backgroundColor: '#e77d7d'
    }

    useEffect(() => { 
        console.log(amountsId)
            if(amountsId === '') {
                setAmounts(undefined)
            }
            else {
                getAmountById(amountsId).then(d => {
                    setAmounts(d)
                    console.log(d)
                })
            }
            
    }, [amountsId])

    const isExceeded = (sugested, total) => {
        return sugested >= total
    }

    return (
        <Table>
            <thead>
                <tr>
                    <th></th>
                    <th>Gasto Base({baseExpencesRate}%)</th>
                    <th>Gasto Adicional({aditionalExpencesRate}%)</th>
                    <th>Ahorro({savingsRate}%)</th>
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
            </tbody>
        </Table>
    )
}
AmountsTableHistorical.propTypes = {
    amountsId: PropTypes.string,
    savingsRate: PropTypes.number,
    baseExpencesRate: PropTypes.number,
    aditionalExpencesRate: PropTypes.number,
};