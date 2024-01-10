import { baseUrl } from "../contants/contants";

export const getCurrentIncomes = async () => {
    return fetch(`${baseUrl}/incomes/current`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const postIncome = async (incomeData) => {
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(incomeData)
    }

    return fetch(`${baseUrl}/income`, options)
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

export const getIncomeById = async (id) => {
    return fetch(`${baseUrl}/income/${id}`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const updateIncome = async (incomeUpdated) => {
    const options = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(incomeUpdated)
    }

    return fetch(`${baseUrl}/income`, options)
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

export const deleteIncome = async (id) => {
    const options = {
        method: 'DELETE',
        mode: 'cors'
    }

    return fetch(`${baseUrl}/income/${id}`, options)
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