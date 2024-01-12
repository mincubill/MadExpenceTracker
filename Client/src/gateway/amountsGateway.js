import { baseUrl } from "../contants/contants";

export const getCurrentAmounts = async (expencesId, incomesId) => {
    return fetch(`${baseUrl}/amount/${expencesId}/${incomesId}`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}

export const getAmountById = async (amountId) => {
    return fetch(`${baseUrl}/amount/${amountId}`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}