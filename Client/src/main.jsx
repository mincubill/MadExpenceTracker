import React from 'react'
import ReactDOM from 'react-dom/client'
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container } from 'react-bootstrap';
import { IncomeForm } from './components/forms/IncomeForm';
import { ExpencesForm } from './components/forms/ExpencesForm';
import { NavigationBar } from './components/NavigationBar';
import { MainTable } from './components/tables/MainTable';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ConfigurationForm } from './components/forms/ConfigurationForm';


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Container>
      <BrowserRouter>
        <NavigationBar />
        <Routes>
          <Route path="/" element={<MainTable />}/>
          <Route path="/expence" element={<ExpencesForm />}/>
          <Route path="/income" element={<IncomeForm />}/>
          <Route path="/configuration" element={<ConfigurationForm />}/>
        </Routes>
      </BrowserRouter>
    </Container>
  </React.StrictMode>,
)
