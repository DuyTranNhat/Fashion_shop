import React from 'react';
import { AttributeGet } from '~/Models/Attribute';

export type Props = {
    attributeList: AttributeGet[];
    handleFilterValues: (value: string, checked: boolean) => void;
};

const AttributeNav = ({ attributeList, handleFilterValues }: Props) => {
    return (
        <div className="col-lg-3 col-md-4">
            {attributeList.map((attribute) => {
                return (
                    <React.Fragment key={attribute.attributeId}>
                        <h5 className="section-title position-relative text-uppercase mb-3">
                            <span className="bg-secondary pr-3">Filter by {attribute.name}</span>
                        </h5>
                        <div className="bg-light p-4 mb-30">
                            <form>
                                {attribute.values.map((value) => (
                                    <div
                                        key={value.valueId}
                                        className="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3"
                                    >
                                        <input
                                            onChange={(e) => handleFilterValues(value.value1, e.target.checked)}
                                            type="checkbox"
                                            className="custom-control-input"
                                            value={value.value1}
                                            id={`value-${value.valueId}`}
                                        />
                                        <label className="custom-control-label" htmlFor={`value-${value.valueId}`}>
                                            {value.value1}
                                        </label>
                                        {/* <span className="badge border font-weight-normal">100</span> */}
                                    </div>
                                ))}
                            </form>
                        </div>
                    </React.Fragment>
                );
            })}
        </div>
    );
};

export default AttributeNav;
