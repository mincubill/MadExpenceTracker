import { Button, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from "moment";
import { useNavigate } from "react-router-dom";
import { EyeFill, Clipboard2Data, Trash2Fill } from "react-bootstrap-icons"
import 'bootstrap/dist/css/bootstrap.min.css'
import { getExpenceById } from "../../gateway/expenceGateway";

export const ExpenseTable = ({data}) => {

    const navigate = useNavigate()

    const viewExpence = (e) => {
        
        getExpenceById(e.currentTarget.id).then(d => {
            const data = {
                id: d.id,
                name: d.name,
                date: d.date,
                expenceType: d.expenceType,
                amount: d.amount
            }
            navigate("/expence", {state: { data, isReadOnly: true } })
           
        })
    }

    return (
        <Table>
            <thead>
                <tr>
                    <th>Nombre</th>
                    <th>Fecha</th>
                    <th>Tipo</th>
                    <th>Valor</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                {data.map(d => (
                    <tr key={d.id}>
                        <td>{d.name}</td>
                        <td>{moment(d.date).format("DD/MM/YYYY")}</td>
                        <td>{d.expenceType === 1 ? "Base" : "Adicional"}</td>
                        <td>{d.amount}</td>
                        <td>
                            <span>
                                <Button 
                                    variant="primary" 
                                    size="sm" 
                                    id={d.id}
                                    className="bi bi-eye" 
                                    onClick={
                                        viewExpence
                                    }
                                >
                                    <EyeFill id={d.id}/>
                                </Button>{' '}
                                <Button variant="warning" size="sm">
                                    <Clipboard2Data/>
                                </Button>{' '}
                                <Button variant="danger" size="sm">
                                    <Trash2Fill/>
                                </Button>
                            </span>
                        </td>
                    </tr>
                ))}

            </tbody>
        </Table>
    )
}

ExpenseTable.propTypes = {
    data: PropTypes.array
};