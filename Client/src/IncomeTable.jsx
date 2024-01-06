import { Table } from "react-bootstrap";
import PropTypes from 'prop-types';

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
                        <td>{d.date}</td>
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