export const formatAmount = (input) => {
    const options2 = { style: 'currency', currency: 'CLP' };
    const numberFormat2 = new Intl.NumberFormat('es-CL', options2);

    return numberFormat2.format(input);
}