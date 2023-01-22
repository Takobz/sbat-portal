interface Response<T> 
{
    data: T,
    errors: Array<string>
}

export default Response;