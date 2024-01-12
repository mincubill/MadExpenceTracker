import { baseUrl } from "../contants/contants";

export const getMonthIndex = async () => {
    return fetch(`${baseUrl}/monthIndex`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}