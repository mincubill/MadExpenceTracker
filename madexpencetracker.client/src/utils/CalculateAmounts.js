export const CalculateAmounts = (expencesData, incomesData) => {

    if (expencesData.length <= 0 && incomesData.length <= 0) {
        return {
            baseExpences: 0,
            incomeExpences: 0,
            savings: 0
        }
    }

    const baseExpences = expencesData.filter(d => d.expenceType === "Base")
        .map(d => d.amount)
        .reduce((a, v) => a + v)
    const aditionalExpences = expencesData.filter(d => d.expenceType === "Aditional")
        .map(d => d.amount)
        .reduce((a, v) => a + v)
    const income = incomesData.map(d => d.amount)
        .reduce((a, v) => a + v)

    const calculateSavings = () => {
        let save = income * 0.2
        if (aditionalExpences < 0) {
            save += aditionalExpences
        }
        return save
    }
    const savings = calculateSavings()

    return { baseExpences, aditionalExpences, income, savings }

}