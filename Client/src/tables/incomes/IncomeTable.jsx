import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";

export const IncomeTable = ({data}) => {
    
    
    return (
        <Table>
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Fecha</th>
                    <th>Valor</th>
                </tr>
            </thead>
            <tbody>
                {data.map(d => (
                    <tr key={d.id}>
                        <td>{d.name}</td>
                        <td>{moment(d.date).format("DD/MM/YYYY")}</td>
                        <td>{d.amount}</td>
                    </tr>
                ))}
            </tbody>
        </Table>
    )
}

IncomeTable.propTypes = {
    data: PropTypes.array
};