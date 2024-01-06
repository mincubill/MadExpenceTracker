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