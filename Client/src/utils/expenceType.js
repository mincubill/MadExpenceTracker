export const getExpenceType = (expenceValue) => {
    const expenceTypes = [{
        value: 1, name: 'Base' 
    },
    {
        value: 2, name: 'Adicional' 
    },
    {
        value: 3, name: 'Ahorro' 
    }]
    const expenceTypeName = expenceTypes.filter(e => e.value === expenceValue)[0].name
    return expenceTypeName !== undefined ? expenceTypeName : 'Desconocido'
}