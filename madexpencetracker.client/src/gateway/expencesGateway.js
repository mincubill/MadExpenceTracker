export const getExpencesGateway = async () => {

    const response = await fetch('expencesApi')
    const data = await response.json()
    console.log(data)
}