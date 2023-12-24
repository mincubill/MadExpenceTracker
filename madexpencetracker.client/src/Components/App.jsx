import { Container } from 'react-bootstrap';
import { IncomeForm } from './Forms/IncomeForm';
import { ExpencesForm } from './Forms/ExpencesForm';
import { NavigationBar } from './NavigationBar';
import { MainTable } from './MainTable';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Configuration } from './Configuration';

export const App = () => {
   
    return (
        <Container>
            <BrowserRouter>
                <NavigationBar />
                <Routes>
                    <Route path="/" element={<MainTable />} />
                    <Route path="/expence" element={<ExpencesForm />} />
                    <Route path="/income" element={<IncomeForm />} />
                    <Route path="/configuration" element={<Configuration />} />
                </Routes>
            </BrowserRouter>
        </Container>
    )
}