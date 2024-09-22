type Props = {
    configs: any;
    data: any;
};

const Table = ({ configs, data }: Props) => {
    const renderedRows = data.map((company : any, index : number) => {
        return (
            <tr key={company.cik}>
                {configs.map((config: any) => {
                    return <td>{config.render(company, index)}</td>
                })}
            </tr>
        );
    });
    
    const renderedHeaders = configs.map((config: any) => {

        return (
            <th
                className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                key={config.label}
            >
                {config.label}
            </th>
        );
    });
    
    return (
                <table className="table">
                    <thead>
                        <tr>
                            {renderedHeaders}
                        </tr>
                    </thead>
                    <tbody>
                        {renderedRows} {/* Gọi hàm renderRows */}
                    </tbody>
                </table>
            

    );

};

export default Table;