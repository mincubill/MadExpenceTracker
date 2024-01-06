import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';

export const AmountsTable = ({data}) => {
    return (
        <Table>
            <thead>
                <tr>
                    <th>Gasto Base</th>
                    <th>Gasto Adicional</th>
                    <th>Ahorro</th>
                    <th>Total ingresos</th>
                </tr>
            </thead>
            <tbody>
                <tr key={data.id}>
                    <td>{data.totalBaseExpences}</td>
                    <td>{data.totalAditionalExpences}</td>
                    <td>{data.savings}</td>
                    <td>{data.totalIncomes}</td>
                </tr>
            </tbody>
        </Table>
    )
}
AmountsTable.propTypes = {
    data: PropTypes.object
};