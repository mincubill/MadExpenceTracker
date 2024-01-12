import { baseUrl } from "../contants/contants";

export const getCurrentExpences = async () => {
    let response = await fetch(`${baseUrl}/expences/current`)
    let data = await response.json()
    return await data
}

export const postExpence = async (expenceData) => {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(expenceData)
    }

    return fetch(`${baseUrl}/expence`, options)
        .then((response) => { 
            if(response.status !== 201) {
                throw Error(response.status)
            }
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const getExpenceById = async (id) => {
    return fetch(`${baseUrl}/expence/${id}`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const updateExpence = async (expenceUpdated) => {
    const options = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(expenceUpdated)
    }

    return fetch(`${baseUrl}/expence`, options)
        .then((response) => { 
            if(response.status !== 202) {
                throw Error(response.status)
            }
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const deleteExpence = async (id) => {
    const options = {
        method: 'DELETE',
        mode: 'cors'
    }

    return fetch(`${baseUrl}/expence/${id}`, options)
        .then((response) => { 
            if(response.status !== 200) {
                throw Error(response.status)
            }
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const getExpencesById = async (id) => {
    return fetch(`${baseUrl}/expences/${id}`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}