import { baseUrl } from "../contants/contants";

export const getCurrentExpences = async () => {
    return fetch(`${baseUrl}/expences/current`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
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
            console.log(response)
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