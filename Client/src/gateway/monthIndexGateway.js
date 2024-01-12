import { baseUrl } from "../contants/contants";

export const getMonthIndex = async () => {
    console.log(baseUrl)
    return fetch(`${baseUrl}/monthIndex`)
        .then((response) => { 
            return response.json().then((data) => {
                return data;
            }).catch((err) => {
                console.log(err);
            }) 
        });
}