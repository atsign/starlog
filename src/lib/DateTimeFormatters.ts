export const timeFormatter = new Intl.DateTimeFormat('en', {
    hour12: true,
    hour: 'numeric',
    minute: '2-digit'
});

export const dateFormatter = new Intl.DateTimeFormat('en', {
    year: 'numeric',
    day: 'numeric',
    month: 'long'
});