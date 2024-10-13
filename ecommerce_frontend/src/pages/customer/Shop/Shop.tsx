import React, { useEffect, useState } from 'react';
import AttributeNav from '~/Components/customer/AttributeNav/AttributeNav';
import Search from '~/Components/customer/Search/Search';
import VariantList from '~/Components/customer/Variant/VariantList';
import Paging from '~/Components/customer/Paging/Paging';
import { AttributeGet } from '~/Models/Attribute';
import { VariantGet } from '~/Models/Variant';
import { attributeGetActiveAPI } from '~/Services/AttributeService';
import { variantGetAPI } from '~/Services/VariantService';

const Shop = () => {
  const [variants, setVariants] = useState<VariantGet[]>([]);
  const [attributes, setAttributes] = useState<AttributeGet[]>([]);
  const [selectedValues, setSelectedValues] = useState<string[]>([]);
  const [filteredVariants, setFilteredVariants] = useState<VariantGet[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [currentPage, setCurrentPage] = useState<number>(1);
  const itemsPerPage = 3;

  useEffect(() => {
    variantGetAPI().then((res) => {
      if (res?.data) {
        setVariants(res?.data);
        setFilteredVariants(res?.data);
      }
    });

    attributeGetActiveAPI().then((res) => {
      if (res?.data) {
        setAttributes(res?.data);
      }
    });
  }, []);

  const handleFilterValues = (value: string, checked: boolean) => {
    if (checked) {
      setSelectedValues((prev) => [...prev, value]);
    } else {
      setSelectedValues((prev) => prev.filter((v) => v !== value));
    }
  };

  const handleFilterName = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  useEffect(() => {
    const handleFilterVariants = () => {
      let filtered = variants;

      // Lọc theo giá trị selectedValues
      if (selectedValues.length > 0) {
        filtered = filtered.filter((variant) =>
          selectedValues.every((value) =>
            variant.values.some((v) => v.value1 === value)
          )
        );
      }

      // Lọc theo tên variant
      if (searchTerm) {
        filtered = filtered.filter((variant) =>
          variant.variantName.toLowerCase().includes(searchTerm.toLowerCase())
        );
      }

      setFilteredVariants(filtered);
    };

    handleFilterVariants();
  }, [selectedValues, variants, searchTerm]);

  // Tính toán các phần tử hiển thị trong trang hiện tại
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentVariants = filteredVariants.slice(indexOfFirstItem, indexOfLastItem);

  return variants.length > 0 && attributes.length > 0 ? (
    <div className="container-fluid">
      <div className="row px-xl-5">
        <AttributeNav handleFilterValues={handleFilterValues} attributeList={attributes} />

        <div className="col-lg-9 col-md-8">
          <div className='row justify-content-center'>
            <Search handleFilterName={handleFilterName} />
          </div>
          <VariantList col={4} variantList={currentVariants} />

          <Paging
            currentPage={currentPage}
            itemsPerPage={itemsPerPage}
            totalItems={filteredVariants.length}
            onPageChange={(page : number) => setCurrentPage(page)}
          />
        </div>
      </div>
    </div>
  ) : (
    <></>
  );
};

export default Shop;
