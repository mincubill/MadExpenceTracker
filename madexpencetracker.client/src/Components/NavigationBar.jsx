import { Container, Navbar, Nav } from "react-bootstrap"
import { Link } from "react-router-dom"

export const NavigationBar = () => {
    return (
        <Navbar>
            <Container>
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
                    <Link to="/configuration" className="nav-link">
                        Configurar
                    </Link>
                </Nav>
            </Container>
        </Navbar>
    )

}