import {Container, Navbar, Nav, Button, Modal } from "react-bootstrap"
import { Link, useNavigate } from "react-router-dom"
import { postCloseMonth } from "../gateway/operationsGateway"
import { Fragment, useState } from "react";
import PropTypes from 'prop-types';

export const NavigationBar = ({setIsMonthClosed}) => {

    const [showConfirmation, setShowConfirmation] = useState(false);
    const [showResult, setShowResult] = useState(false);
    const [resultMessage, setResultMessage] = useState('');

    const handleCloseConfirmation = () => setShowConfirmation(false);
    const handleCloseResult = () => setShowResult(false);
    const handleShowConfirmation = () => setShowConfirmation(true);
    const handleShowResult = () => setShowResult(true);

    const closeMonth = () => {
        handleCloseConfirmation()
        const monthToClose = localStorage.getItem("monthToClose")
        const expencesId = localStorage.getItem("expencesId")
        const incomesId = localStorage.getItem("incomesId")
        postCloseMonth(monthToClose, expencesId, incomesId).then(d => {
            console.log(d)
            if(d) {
                setResultMessage('Mes cerrado con exito')
                handleShowResult()
                setIsMonthClosed(true)
            }
            else {
                setResultMessage('Ocurrio un error')
                handleShowResult()
            }
        })
    }

    const onCloseResult = () => {
        handleCloseResult()
    }

    const navigate = useNavigate();

    const handleRedirect = () => {
        navigate("/configuration", {state: {isConfigured:true}})
    }

    return (
        <Fragment>
            <Navbar expand="lg" className="bg-body-tertiary">
                <Container>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Link to="/" className="nav-link">
                                Resumen
                            </Link>
                            <Link to="/expence" className="nav-link">
                                    Gasto
                            </Link>
                            <Link to="/income" className="nav-link">
                                Ingresos
                            </Link>
                            <Link to="/historical" className="nav-link">
                                Historial
                            </Link>
                            <Nav.Link className="nav-link" onClick={handleRedirect}>
                                Configuracion
                            </Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                    
                    <Button variant="warning" onClick={handleShowConfirmation}>Cerrar mes</Button>
                </Container>
            </Navbar>

            <Modal show={showConfirmation} onHide={handleCloseConfirmation}>
                <Modal.Header closeButton>
                <Modal.Title>Advertencia</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Deseas realizar el cierre de mes?
                    Esta operacion no se puede deshacer (osea, si pero hay q modificar la db xD)
                </Modal.Body>
                <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseConfirmation}>
                    Cancelar
                </Button>
                <Button variant="primary" onClick={closeMonth}>
                    Cerrar mes
                </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={showResult} onHide={onCloseResult}>
                <Modal.Header closeButton>
                <Modal.Title>Advertencia</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {resultMessage}
                </Modal.Body>
                <Modal.Footer>
                <Button variant="primary" onClick={onCloseResult}>
                    OK
                </Button>
                </Modal.Footer>
            </Modal>
        </Fragment>   
    )
}

NavigationBar.propTypes = {
    setIsMonthClosed: PropTypes.func
};