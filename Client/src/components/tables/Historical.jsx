import { useEffect, useState } from "react"
import { Card, Nav } from "react-bootstrap"
import { getMonthIndex } from "../../gateway/monthIndexGateway"
import { getExpencesById } from "../../gateway/expenceGateway"
import { getAmountById, getCurrentAmounts } from "../../gateway/amountsGateway"
import { getIncomesById } from "../../gateway/incomesGateway"
import { MainTable } from "./MainTable"
import { MainTableHistorical } from "./MainTableHistorical"


export const Historical = () => {
    const [monthIndexes, setMonthIndexes] = useState([])
    const [expencesIdData, setExpencesIdData] = useState('')
    const [incomesIdData, setIncomesIdData] = useState('')
    const [amountsIdData, setAmountsIdData] = useState('')
    const [savingsRateData, setSavingsRateData] = useState(0)
    const [baseExpencesRateData, setBaseExpencesRateData] = useState(0)
    const [aditionalExpencesRateData, setAditionalExpencesRateData] = useState(0)
    const [recentId, setRecentId] = useState('')

    useEffect(() => {
        getMonthIndex().then(d => {
            setMonthIndexes(d.monthIndex)
        })
        
    }, [])



    const tabStyle = {
        flexWrap: "nowrap",
        overflowX: "auto"
    }

    const getHistory = (e) => {
        const {expencesId, 
            incomesId, 
            amountsId, 
            savingsRate, 
            baseExpencesRate, 
            aditionalExpencesRate} = monthIndexes.find(d => d.id === e.currentTarget.id)
        setSavingsRateData(savingsRate)
        setBaseExpencesRateData(baseExpencesRate)
        setAditionalExpencesRateData(aditionalExpencesRate)
        setAmountsIdData(amountsId)
        setExpencesIdData(expencesId)
        setIncomesIdData(incomesId)
    }

    return (
        <Card>
            <Card.Header>
                <Card.Title>Historial</Card.Title>
                <Nav variant="pills" navbarScroll={true} defaultActiveKey="#first" style={tabStyle}>
                    { monthIndexes ? monthIndexes.map((d, i) => 
                        <Nav.Item key={d.month}>
                            <Nav.Link
                                id={d.id} 
                                eventKey={i === 0 ? "#first": `${d.month}`}
                                onClick={getHistory}>
                                    {d.month}
                                </Nav.Link>
                        </Nav.Item>
                    ) : "<b>no hay meses cerrados</b>"}
                </Nav>
            </Card.Header>
            <Card.Body>
                <MainTableHistorical
                    savingsRate={savingsRateData}
                    baseExpencesRate={baseExpencesRateData}
                    aditionalExpencesRate={aditionalExpencesRateData}
                    amountsId={amountsIdData}
                    expencesId={expencesIdData}
                    incomesId={incomesIdData}
                />
            </Card.Body>

        </Card>
    )
}