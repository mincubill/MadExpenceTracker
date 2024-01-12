/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react"
import { Card, Nav } from "react-bootstrap"
import { getMonthIndex } from "../../gateway/monthIndexGateway"
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
            if(d.monthIndex) {
                setRecentId(d.monthIndex.id)
                getHistory(d.monthIndex[0].id)
            }
        })
    }, [recentId])

    const tabStyle = {
        flexWrap: "nowrap",
        overflowX: "auto"
    }

    const getHistory = (id) => {
        if(monthIndexes.length === 0) 
        {
            return
        }
        const {expencesId, 
            incomesId, 
            amountsId, 
            savingsRate, 
            baseExpencesRate, 
            aditionalExpencesRate} = monthIndexes.find(d => d.id === id)
        setSavingsRateData(savingsRate)
        setBaseExpencesRateData(baseExpencesRate)
        setAditionalExpencesRateData(aditionalExpencesRate)
        setAmountsIdData(amountsId)
        setExpencesIdData(expencesId)
        setIncomesIdData(incomesId)
    }

    const handleGetHistory = (e) => {
        getHistory(e.currentTarget.id)
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
                                onClick={handleGetHistory}>
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