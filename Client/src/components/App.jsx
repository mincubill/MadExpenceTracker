import { Container } from "react-bootstrap"
import { BrowserRouter, Route, Routes } from "react-router-dom"
import { NavigationBar } from "./NavigationBar"
import { MainTable } from "./tables/MainTable"
import { ExpencesForm } from "./forms/ExpencesForm"
import { IncomeForm } from "./forms/IncomeForm"
import { ConfigurationForm } from "./forms/ConfigurationForm"
import { useState } from "react"
import { Historical } from "./tables/Historical"

export const App = () => {
    const [isMonthClosed, setIsMonthClosed] = useState(false)
    const [isHistorical] = useState(false)
    return(
        <Container>
            <BrowserRouter>
                <NavigationBar setIsMonthClosed={setIsMonthClosed} />
                <Routes>
                <Route path="/" element={<MainTable isMonthClosed={isMonthClosed} isHistorical={isHistorical}/>}/>
                <Route path="/expence" element={<ExpencesForm />}/>
                <Route path="/income" element={<IncomeForm />}/>
                <Route path="/historical" element={<Historical />}/>
                <Route path="/configuration" element={<ConfigurationForm />}/>
                </Routes>
            </BrowserRouter>
        </Container>
    )
}