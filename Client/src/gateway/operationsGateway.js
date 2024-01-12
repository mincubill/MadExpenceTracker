import { baseUrl } from "../contants/contants";

export const postCloseMonth = async (monthToClose, expencesId, incomesId) => {
    const resumeData = {
        monthToClose,
        expencesId,
        incomesId
    }
    
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        mode: 'cors',
        body: JSON.stringify(resumeData)
    }

    return fetch(`${baseUrl}/monthClose`, options)
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