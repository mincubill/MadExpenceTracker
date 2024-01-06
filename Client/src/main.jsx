import React from 'react'
import ReactDOM from 'react-dom/client'
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container } from 'react-bootstrap';
import { IncomeForm } from './forms/IncomeForm';
import { ExpencesForm } from './forms/ExpencesForm';
import { NavigationBar } from './NavigationBar';
import { MainTable } from './tables/MainTable';
import { BrowserRouter, Routes, Route } from 'react-router-dom';


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Container>
      <BrowserRouter>
        <NavigationBar />
        <Routes>
          <Route path="/" element={<MainTable />}/>
          <Route path="/expence" element={<ExpencesForm />}/>
          <Route path="/income" element={<IncomeForm />}/>
        </Routes>
      </BrowserRouter>
    </Container>
  </React.StrictMode>,
)
