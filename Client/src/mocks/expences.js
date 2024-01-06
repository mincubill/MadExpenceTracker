import { v4 as uuidv4 } from 'uuid';

export const expences = [
        {
            id: uuidv4(),
            name: 'almacen',
            date: "2023/12/01",
            type: 'Expence',
            expenceType: 'Aditional',
            amount: 5790
        },
        {
            id: uuidv4(),
            name: 'agua',
            date: "2023/12/01",
            expenceType: 'Base',
            type: 'Expence',
            amount: 7890
        },
        {
            id: uuidv4(),
            name: 'cafe',
            date: "2023/12/05",
            expenceType: 'Aditional',
            type: 'Expence',
            amount: 8990
        },
        {
            id: uuidv4(),
            name: 'super',
            date: "2023/12/05",
            expenceType: 'Aditional',
            type: 'Expence',
            amount: 19820
        },
        {
            id: uuidv4(),
            name: 'almacen',
            date: "2023/12/13",
            expenceType: 'Aditional',
            type: 'Expence',
            amount: 5790
        }
]

