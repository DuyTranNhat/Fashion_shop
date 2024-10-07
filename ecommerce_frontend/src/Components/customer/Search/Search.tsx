import React from 'react';

export type Props = {
    handleFilterName: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const Search = ({ handleFilterName }: Props) => {
    return (
        <form className='mb-4' style={{maxWidth: "700px"}} >
            <div className="input-group">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Search for variants"
                    onChange={handleFilterName}
                />
                <div className="input-group-append">
                    <span className="input-group-text bg-transparent text-primary">
                        <i className="fa fa-search"></i>
                    </span>
                </div>
            </div>
        </form>
    );
}

export default Search;
